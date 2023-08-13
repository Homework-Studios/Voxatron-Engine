package homeworkstudios.language;

import com.intellij.openapi.util.TextRange;
import com.intellij.patterns.PlatformPatterns;
import com.intellij.psi.*;
import com.intellij.util.ProcessingContext;
import org.jetbrains.annotations.NotNull;

import static homeworkstudios.language.VoxaScriptAnnotator.VoxaScript_PREFIX_STR;
import static homeworkstudios.language.VoxaScriptAnnotator.VoxaScript_SEPARATOR_STR;

public class VoxaScriptReferenceContributor extends PsiReferenceContributor {

    @Override
    public void registerReferenceProviders(@NotNull PsiReferenceRegistrar registrar) {
        registrar.registerReferenceProvider(PlatformPatterns.psiElement(PsiLiteralExpression.class),
                new PsiReferenceProvider() {
                    @Override
                    public PsiReference @NotNull [] getReferencesByElement(@NotNull PsiElement element,
                                                                           @NotNull ProcessingContext context) {
                        PsiLiteralExpression literalExpression = (PsiLiteralExpression) element;
                        String value = literalExpression.getValue() instanceof String ?
                                (String) literalExpression.getValue() : null;
                        if ((value != null && value.startsWith(VoxaScript_PREFIX_STR + VoxaScript_SEPARATOR_STR))) {
                            TextRange property = new TextRange(VoxaScript_PREFIX_STR.length() + VoxaScript_SEPARATOR_STR.length() + 1,
                                    value.length() + 1);
                            return new PsiReference[]{new VoxaScriptReference(element, property)};
                        }
                        return PsiReference.EMPTY_ARRAY;
                    }
                });
    }

}
