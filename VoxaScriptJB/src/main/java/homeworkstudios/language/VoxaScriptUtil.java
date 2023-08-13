package homeworkstudios.language;

import com.google.common.collect.Lists;
import com.intellij.openapi.project.Project;
import com.intellij.openapi.util.text.StringUtil;
import com.intellij.openapi.vfs.VirtualFile;
import com.intellij.psi.PsiComment;
import com.intellij.psi.PsiElement;
import com.intellij.psi.PsiManager;
import com.intellij.psi.PsiWhiteSpace;
import com.intellij.psi.search.FileTypeIndex;
import com.intellij.psi.search.GlobalSearchScope;
import com.intellij.psi.util.PsiTreeUtil;
import homeworkstudios.language.psi.VoxaScriptProperty;
import org.jetbrains.annotations.NotNull;

import java.util.*;

public class VoxaScriptUtil {

    /**
     * Searches the entire project for VoxaScript language files with instances of the VoxaScript property with the given key.
     *
     * @param project current project
     * @param key     to check
     * @return matching properties
     */
    public static List<VoxaScriptProperty> findProperties(Project project, String key) {
        List<VoxaScriptProperty> result = new ArrayList<>();
        Collection<VirtualFile> virtualFiles =
                FileTypeIndex.getFiles(VoxaScriptFileType.INSTANCE, GlobalSearchScope.allScope(project));
        for (VirtualFile virtualFile : virtualFiles) {
            VoxaScriptFile VoxaScriptFile = (VoxaScriptFile) PsiManager.getInstance(project).findFile(virtualFile);
            if (VoxaScriptFile != null) {
                VoxaScriptProperty[] properties = PsiTreeUtil.getChildrenOfType(VoxaScriptFile, VoxaScriptProperty.class);
                if (properties != null) {
                    for (VoxaScriptProperty property : properties) {
                        if (key.equals(property.getKey())) {
                            result.add(property);
                        }
                    }
                }
            }
        }
        return result;
    }

    public static List<VoxaScriptProperty> findProperties(Project project) {
        List<VoxaScriptProperty> result = new ArrayList<>();
        Collection<VirtualFile> virtualFiles =
                FileTypeIndex.getFiles(VoxaScriptFileType.INSTANCE, GlobalSearchScope.allScope(project));
        for (VirtualFile virtualFile : virtualFiles) {
            VoxaScriptFile VoxaScriptFile = (VoxaScriptFile) PsiManager.getInstance(project).findFile(virtualFile);
            if (VoxaScriptFile != null) {
                VoxaScriptProperty[] properties = PsiTreeUtil.getChildrenOfType(VoxaScriptFile, VoxaScriptProperty.class);
                if (properties != null) {
                    Collections.addAll(result, properties);
                }
            }
        }
        return result;
    }

    /**
     * Attempts to collect any comment elements above the VoxaScript key/value pair.
     */
    public static @NotNull String findDocumentationComment(VoxaScriptProperty property) {
        List<String> result = new LinkedList<>();
        PsiElement element = property.getPrevSibling();
        while (element instanceof PsiComment || element instanceof PsiWhiteSpace) {
            if (element instanceof PsiComment) {
                String commentText = element.getText().replaceFirst("//", "");
                result.add(commentText);
            }
            element = element.getPrevSibling();
        }
        return StringUtil.join(Lists.reverse(result), "\n ");
    }

}