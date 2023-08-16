// This is a generated file. Not intended for manual editing.
package homeworkstudios.language.psi.impl;

import java.util.List;
import org.jetbrains.annotations.*;
import com.intellij.lang.ASTNode;
import com.intellij.psi.PsiElement;
import com.intellij.psi.PsiElementVisitor;
import com.intellij.psi.util.PsiTreeUtil;
import static homeworkstudios.language.psi.VoxaScriptTypes.*;
import com.intellij.extapi.psi.ASTWrapperPsiElement;
import homeworkstudios.language.psi.*;

public class VoxaScriptDefaultImpl extends ASTWrapperPsiElement implements VoxaScriptDefault {

  public VoxaScriptDefaultImpl(@NotNull ASTNode node) {
    super(node);
  }

  public void accept(@NotNull VoxaScriptVisitor visitor) {
    visitor.visitDefault(this);
  }

  @Override
  public void accept(@NotNull PsiElementVisitor visitor) {
    if (visitor instanceof VoxaScriptVisitor) accept((VoxaScriptVisitor)visitor);
    else super.accept(visitor);
  }

}
