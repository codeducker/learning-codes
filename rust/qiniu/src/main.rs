use qiniu_sdk::upload_token::{credential::Credential, prelude::*, UploadPolicy};
use std::time::Duration;

fn main() {
    let access_key = "";
    let secret_key = "";
    let bucket_name = "";
    let credential = Credential::new(access_key, secret_key);
    let upload_token = UploadPolicy::new_for_bucket(bucket_name, Duration::from_secs(3600))
        .build_token(credential, Default::default())?;
    println!("{}", upload_token);
}

// curl -X POST "localhost:9200/_security/oauth2/token?pretty" -H 'Content-Type: application/json' -d'
// {
//   "grant_type" : "password",
//   "username" : "test_admin",
//   "password" : "x-pack-test-password"
// }
// '
