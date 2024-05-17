package com.loern.tasks;

import org.gradle.api.file.DirectoryProperty;
import org.gradle.api.model.ObjectFactory;
import org.gradle.api.tasks.*;
import org.gradle.api.DefaultTask;
import org.gradle.api.provider.Property;
import org.gradle.api.tasks.Input;
import org.gradle.api.provider.Provider;
import org.gradle.workers.WorkerExecutor;

import javax.inject.Inject;
import java.net.URI;


public abstract class Download extends DefaultTask{

    public final DirectoryProperty directoryProperty;

    @Input
    public abstract Property<String> getFileText();

    @Inject
    public Download(ObjectFactory objectFactory){
        this.directoryProperty = objectFactory.directoryProperty();
    }

    @OutputDirectory
    public DirectoryProperty getOutputDirectory(){
        return directoryProperty;
    }

    //Property Inject
    @Inject
    public abstract WorkerExecutor getWorkerExecutor();


    @Input
    public abstract Property<String> getLocation();

    @Nested//Property Nested Inject
    public abstract Resource getResource();

    @Internal
    public Provider<URI> getUri() {
        return getLocation().map(l -> URI.create("https://" + l));
    }
    
    @TaskAction
    public void run(){
        System.out.println("Download url :"+getUri().get()+" is processing.");
        System.out.println("Downing https://"+getResource().getHostName() +"/"+getResource().getPath());
    }
}