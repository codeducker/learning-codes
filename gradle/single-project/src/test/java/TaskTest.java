import org.gradle.api.Project;
import org.gradle.api.Task;
import org.gradle.internal.impldep.org.junit.Assert;
import org.gradle.testfixtures.ProjectBuilder;
import org.junit.jupiter.api.Test;

public class TaskTest {

    @Test
    public void testTask(){
        ProjectBuilder builder = ProjectBuilder.builder();
        Project project = builder.build();
        Task task = project.getTasks().create("greeting", GreetingTask.class);
        Assert.assertTrue(task instanceof GreetingTask);
    }
}
