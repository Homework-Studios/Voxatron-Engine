// This is a generated file. Not intended for manual editing.
package homeworkstudios.language.psi.impl;

import java.util.List;
import org.jetbrains.annotations.*;
import com.intellij.lang.ASTNode;
import com.intellij.psi.PsiElement;
import com.intellij.psi.PsiElementVisitor;
import com.intellij.psi.util.PsiTreeUtil;
import static homeworkstudios.language.psi.VoxaScriptTypes.*;
import homeworkstudios.language.psi.*;
import com.intellij.navigation.ItemPresentation;

public class VoxaScriptPropertyImpl extends VoxaScriptNamedElementImpl implements VoxaScriptProperty {

  public VoxaScriptPropertyImpl(@NotNull ASTNode node) {
    super(node);
  }

  public void accept(@NotNull VoxaScriptVisitor visitor) {
    visitor.visitProperty(this);
  }

  @Override
  public void accept(@NotNull PsiElementVisitor visitor) {
    if (visitor instanceof VoxaScriptVisitor) accept((VoxaScriptVisitor)visitor);
    else super.accept(visitor);
  }

  @Override
  public String getKey() {
    return VoxaScriptPsiImplUtil.getKey(this);
  }

  @Override
  public String getValue() {
    return VoxaScriptPsiImplUtil.getValue(this);
  }

  @Override
  public String getName() {
    return VoxaScriptPsiImplUtil.getName(this);
  }

  @Override
  public PsiElement setName(String newName) {
    return VoxaScriptPsiImplUtil.setName(this, newName);
  }

  @Override
  public PsiElement getNameIdentifier() {
    return VoxaScriptPsiImplUtil.getNameIdentifier(this);
  }

  @Override
  public ItemPresentation getPresentation() {
    return VoxaScriptPsiImplUtil.getPresentation(this);
  }

}
