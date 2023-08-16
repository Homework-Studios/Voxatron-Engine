// This is a generated file. Not intended for manual editing.
package homeworkstudios.language.psi;

import org.jetbrains.annotations.*;
import com.intellij.psi.PsiElementVisitor;
import com.intellij.psi.PsiElement;

public class VoxaScriptVisitor extends PsiElementVisitor {

  public void visitBlockend(@NotNull VoxaScriptBlockend o) {
    visitPsiElement(o);
  }

  public void visitBlockstart(@NotNull VoxaScriptBlockstart o) {
    visitPsiElement(o);
  }

  public void visitCodeblock(@NotNull VoxaScriptCodeblock o) {
    visitPsiElement(o);
  }

  public void visitComma(@NotNull VoxaScriptComma o) {
    visitPsiElement(o);
  }

  public void visitDefault(@NotNull VoxaScriptDefault o) {
    visitPsiElement(o);
  }

  public void visitNumber(@NotNull VoxaScriptNumber o) {
    visitPsiElement(o);
  }

  public void visitProperty(@NotNull VoxaScriptProperty o) {
    visitNamedElement(o);
  }

  public void visitString(@NotNull VoxaScriptString o) {
    visitPsiElement(o);
  }

  public void visitNamedElement(@NotNull VoxaScriptNamedElement o) {
    visitPsiElement(o);
  }

  public void visitPsiElement(@NotNull PsiElement o) {
    visitElement(o);
  }

}
