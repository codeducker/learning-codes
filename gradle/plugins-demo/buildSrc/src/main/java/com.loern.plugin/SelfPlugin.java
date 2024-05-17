package com.loern.plugin;

import org.gradle.api.*;
import org.gradle.api.plugins.BasePlugin;

public class SelfPlugin implements Plugin<Project> {
    @Override
    public void apply(Project project) {
        project.getPluginManager().apply(BasePlugin.class);
    }
}