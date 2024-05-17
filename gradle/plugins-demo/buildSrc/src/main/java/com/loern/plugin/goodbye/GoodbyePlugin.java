package com.loern.plugin.goodbye;

import groovy.lang.Closure;
import org.gradle.api.Action;
import org.gradle.api.Plugin;
import org.gradle.api.Project;
import org.gradle.api.Task;
import org.gradle.api.plugins.BasePlugin;

public class GoodbyePlugin implements Plugin<Project> {
    @Override
    public void apply(Project project) {
        Task goodsByte = project.getTasks().create("GoodBye");
//        goodsByte.configure(new Closure(goodsByte) {
//            @Override
//            public Object call() {
//                return super.call();
//            }
//        });
        goodsByte.doLast(new Action<Task>() {
            @Override
            public void execute(Task task) {
                System.out.println("GoodBye!");
            }
        });
        project.getPluginManager().apply(BasePlugin.class);
    }
}
