package homeworkstudios.language.psi.impl;

import com.intellij.extapi.psi.ASTWrapperPsiElement;
import com.intellij.lang.ASTNode;
import homeworkstudios.language.psi.VoxaScriptNamedElement;
import org.jetbrains.annotations.NotNull;

public abstract class VoxaScriptNamedElementImpl extends ASTWrapperPsiElement implements VoxaScriptNamedElement {

    public VoxaScriptNamedElementImpl(@NotNull ASTNode node) {
        super(node);
    }

}
