package org.gradle.accessors.dm;

import org.gradle.api.NonNullApi;
import org.gradle.api.artifacts.ProjectDependency;
import org.gradle.api.internal.artifacts.dependencies.ProjectDependencyInternal;
import org.gradle.api.internal.artifacts.DefaultProjectDependencyFactory;
import org.gradle.api.internal.artifacts.dsl.dependencies.ProjectFinder;
import org.gradle.api.internal.catalog.DelegatingProjectDependency;
import org.gradle.api.internal.catalog.TypeSafeProjectDependencyFactory;
import javax.inject.Inject;

@NonNullApi
public class ServiceProjectDependency extends DelegatingProjectDependency {

    @Inject
    public ServiceProjectDependency(TypeSafeProjectDependencyFactory factory, ProjectDependencyInternal delegate) {
        super(factory, delegate);
    }

    /**
     * Creates a project dependency on the project at path ":service:api-service"
     */
    public Service_ApiServiceProjectDependency getApiService() { return new Service_ApiServiceProjectDependency(getFactory(), create(":service:api-service")); }

    /**
     * Creates a project dependency on the project at path ":service:file-dependencies"
     */
    public Service_FileDependenciesProjectDependency getFileDependencies() { return new Service_FileDependenciesProjectDependency(getFactory(), create(":service:file-dependencies")); }

    /**
     * Creates a project dependency on the project at path ":service:jasper-service"
     */
    public Service_JasperServiceProjectDependency getJasperService() { return new Service_JasperServiceProjectDependency(getFactory(), create(":service:jasper-service")); }

}
