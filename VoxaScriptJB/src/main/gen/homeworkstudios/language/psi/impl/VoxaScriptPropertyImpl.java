// This is a generated file. Not intended for manual editing.
package homeworkstudios.language.psi.impl;

import com.intellij.extapi.psi.ASTWrapperPsiElement;
import com.intellij.lang.ASTNode;
import com.intellij.psi.PsiElementVisitor;
import homeworkstudios.language.psi.VoxaScriptProperty;
import homeworkstudios.language.psi.VoxaScriptVisitor;
import org.jetbrains.annotations.NotNull;

public class VoxaScriptPropertyImpl extends ASTWrapperPsiElement implements VoxaScriptProperty {

    public VoxaScriptPropertyImpl(@NotNull ASTNode node) {
        super(node);
    }


    @Override
    public void accept(@NotNull PsiElementVisitor visitor) {
        if (visitor instanceof VoxaScriptVisitor) accept(visitor);
        else super.accept(visitor);
    }

}
