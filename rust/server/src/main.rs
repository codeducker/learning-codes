use regex::Regex;
use reqwest::Client;
use serde_json::json;
use std::borrow::Cow;
use std::string::FromUtf8Error;
use tokio::io::AsyncReadExt;
use tokio::net::TcpListener;
use urlencoding::decode;

async fn send_post_request(content: String) -> Result<(), reqwest::Error> {
    println!("call ....");
    let client = Client::new();
    let url = "https://oapi.dingtalk.com/robot/send?access_token=xxx";

    let json = json!({
        "text": {
            "content":format!("{} {}","通知", content)
        },
        "msgtype": "text"
    });

    let response = client.post(url).json(&json).send().await?;

    println!("Response status: {}", response.status());

    let body = response.text().await?;
    println!("Response body: {:?}", body);

    Ok(())
}

fn get_vec(result: Result<Cow<str>, FromUtf8Error>) -> Vec<u8> {
    match result {
        Ok(Cow::Borrowed(s)) => s.to_owned().into_bytes(),
        Ok(Cow::Owned(s)) => s.into_bytes(),
        Err(e) => e.into_bytes(),
    }
}

#[tokio::main]
async fn main() -> Result<(), Box<dyn std::error::Error>> {
    let listener = TcpListener::bind("127.0.0.1:8080").await?;

    loop {
        let (mut socket, _) = listener.accept().await?;

        tokio::spawn(async move {
            let mut buf = [0; 1024];

            // In a loop, read data from the socket and write the data back.
            loop {
                let _n = match socket.read(&mut buf).await {
                    // socket closed
                    Ok(n) if n == 0 => return,
                    Ok(n) => {
                        if let Ok(s) = std::str::from_utf8(&buf[..n]) {
                            println!("Reviced message :{}", s);
                            let re = Regex::new(r"GET \/\?value=(.+)\ HTTP/1.1").unwrap();
                            let mut values = vec![];
                            for (_, [value]) in re.captures_iter(s).map(|c| c.extract()) {
                                values.push(value);
                            }
                            let str = values[0].to_string();
                            let origin_str = decode(str.as_str());
                            let keyword = String::from_utf8(get_vec(origin_str)).unwrap();
                            let new_keyword = keyword.replace("&&_json_att=", "");
                            _ = send_post_request(new_keyword).await;
                            println!("Start to Call DingTalk...");
                        }
                        return;
                    }
                    Err(e) => {
                        eprintln!("failed to read from socket; err = {:?}", e);
                        return;
                    }
                };

                // // Write the data back
                // if let Err(e) = socket.write_all(&buf[..n]).await {
                //     eprintln!("failed to write to socket; err = {:?}", e);
                //     return;
                // }
            }
        });
    }
}
