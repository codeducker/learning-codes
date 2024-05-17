import org.gradle.api.tasks.*;
import org.gradle.api.Action;
import org.gradle.api.file.ConfigurableFileCollection;
import org.gradle.api.file.DirectoryProperty;
import org.gradle.api.file.RegularFile;
import org.gradle.api.provider.Provider;
import org.gradle.workers.*;
import org.gradle.process.JavaForkOptions;

import javax.inject.Inject;
import java.io.File;
import java.util.Set;


public abstract class CreateMD5 extends SourceTask{
    
    @InputFiles
    abstract public ConfigurableFileCollection getCodecClasspath();
    
    @OutputDirectory
    abstract public DirectoryProperty getDestinationDirectory();
    
    @Inject
    abstract public WorkerExecutor getWorkerExector();
    
    @TaskAction
    public void createHashes(){
        WorkQueue workQueue = getWorkerExector().processIsolation(workerSpec -> {
            workerSpec.getClasspath().from(getCodecClasspath());
            workerSpec.forkOptions(options-> {
               options.setMaxHeapSize("64m"); 
            });
        });
        for (final File sourceFile : getSource().getFiles()){
            final Provider<RegularFile> md5File = getDestinationDirectory().file(sourceFile.getName() + ".md5");
            workQueue.submit(GenerateMD5.class,parameters->{
                parameters.getSourceFile().set(sourceFile);
                parameters.getMD5File().set(md5File);
            });
        }
    }
}