package homeworkstudios.language;

import com.intellij.ide.IconProvider;
import com.intellij.psi.PsiElement;
import homeworkstudios.language.psi.VoxaScriptProperty;
import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;

import javax.swing.*;

public class VoxaScriptPropertyIconProvider extends IconProvider {

    @Override
    public @Nullable Icon getIcon(@NotNull PsiElement element, int flags) {
        return element instanceof VoxaScriptProperty ? VoxaScriptIcons.FILE : null;
    }

}
