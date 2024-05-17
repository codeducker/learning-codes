
//自定义命令行参数
import org.gradle.api.tasks.options.Option;
import org.gradle.api.tasks.options.OptionValues;
import org.gradle.api.DefaultTask;
import org.gradle.api.provider.Property;
import org.gradle.api.tasks.Input;
import org.gradle.api.tasks.TaskAction;
import java.util.List;
import java.util.ArrayList;
import java.util.Arrays;

public abstract class VerifyUrl extends DefaultTask {
    private String url;
    private OutputType outputType;

    @Input
    @Option(option="http",description="Configures if the http protocol is allowed")
    public abstract Property<Boolean> getHttp();

    @Option(option = "url", description = "Configures the URL to be verified.")
    public void setUrl(String url) {
        if(!getHttp().getOrElse(true) && url.startsWith("http://")){
            throw new IllegalArgumentException("Http is not Allowed");
        }else{
            this.url = url;
        }
    }

    @Input
    public String getUrl() {
        return url;
    }

    @Option(option="output-type" ,description="Configures the output type")
    public void setOutputType(OutputType outputType){
        this.outputType = outputType;
    }

    @OptionValues("output-type")
    public List<OutputType> getAvailableOutputTypes(){
        return new ArrayList<OutputType>(Arrays.asList(OutputType.values()));
    }

    @Input
    public OutputType getOutputType(){
        return outputType;
    }

    public static enum OutputType {
        CONSOLE,FILE;
    }

    @TaskAction
    public void verify() {
        getLogger().quiet("Verifying URL '{}', OutputType : '{}'", url,outputType);
    }
}