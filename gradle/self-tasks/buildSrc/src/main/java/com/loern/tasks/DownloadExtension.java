package com.loern.tasks;

import org.gradle.api.model.ObjectFactory;

import javax.inject.Inject;

public class DownloadExtension {

    private final Resource resource;

    @Inject
    public DownloadExtension(ObjectFactory objectFactory){
        resource = objectFactory.newInstance(Resource.class);
    }

    public Resource getResource(){
        return resource;
    }
}

