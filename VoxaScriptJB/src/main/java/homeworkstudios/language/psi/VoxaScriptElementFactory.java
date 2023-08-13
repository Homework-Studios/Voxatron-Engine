package homeworkstudios.language.psi;

import com.intellij.openapi.project.Project;
import com.intellij.psi.PsiElement;
import com.intellij.psi.PsiFileFactory;
import homeworkstudios.language.VoxaScriptFile;
import homeworkstudios.language.VoxaScriptFileType;

public class VoxaScriptElementFactory {

    public static VoxaScriptProperty createProperty(Project project, String name) {
        final VoxaScriptFile file = createFile(project, name);
        return (VoxaScriptProperty) file.getFirstChild();
    }

    public static VoxaScriptFile createFile(Project project, String text) {
        String name = "dummy.VoxaScript";
        return (VoxaScriptFile) PsiFileFactory.getInstance(project).createFileFromText(name, VoxaScriptFileType.INSTANCE, text);
    }

    public static VoxaScriptProperty createProperty(Project project, String name, String value) {
        final VoxaScriptFile file = createFile(project, name + " = " + value);
        return (VoxaScriptProperty) file.getFirstChild();
    }

    public static PsiElement createCRLF(Project project) {
        final VoxaScriptFile file = createFile(project, "\n");
        return file.getFirstChild();
    }

}