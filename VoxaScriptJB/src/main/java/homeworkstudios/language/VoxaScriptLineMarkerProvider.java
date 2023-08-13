package homeworkstudios.language;

import com.intellij.codeInsight.daemon.RelatedItemLineMarkerInfo;
import com.intellij.codeInsight.daemon.RelatedItemLineMarkerProvider;
import com.intellij.codeInsight.navigation.NavigationGutterIconBuilder;
import com.intellij.openapi.project.Project;
import com.intellij.psi.PsiElement;
import com.intellij.psi.PsiLiteralExpression;
import com.intellij.psi.impl.source.tree.java.PsiJavaTokenImpl;
import homeworkstudios.language.psi.VoxaScriptProperty;
import org.jetbrains.annotations.NotNull;

import java.util.Collection;
import java.util.List;

public class VoxaScriptLineMarkerProvider extends RelatedItemLineMarkerProvider {

    @Override
    protected void collectNavigationMarkers(@NotNull PsiElement element,
                                            @NotNull Collection<? super RelatedItemLineMarkerInfo<?>> result) {
        // This must be an element with a literal expression as a parent
        if (!(element instanceof PsiJavaTokenImpl) || !(element.getParent() instanceof PsiLiteralExpression literalExpression)) {
            return;
        }

        // The literal expression must start with the VoxaScript language literal expression
        String value = literalExpression.getValue() instanceof String ? (String) literalExpression.getValue() : null;
        if ((value == null) ||
                !value.startsWith(VoxaScriptAnnotator.VoxaScript_PREFIX_STR + VoxaScriptAnnotator.VoxaScript_SEPARATOR_STR)) {
            return;
        }

        // Get the VoxaScript language property usage
        Project project = element.getProject();
        String possibleProperties = value.substring(
                VoxaScriptAnnotator.VoxaScript_PREFIX_STR.length() + VoxaScriptAnnotator.VoxaScript_SEPARATOR_STR.length()
        );
        final List<VoxaScriptProperty> properties = VoxaScriptUtil.findProperties(project, possibleProperties);
        if (properties.size() > 0) {
            // Add the property to a collection of line marker info
            NavigationGutterIconBuilder<PsiElement> builder =
                    NavigationGutterIconBuilder.create(VoxaScriptIcons.FILE)
                            .setTargets(properties)
                            .setTooltipText("Navigate to VoxaScript language property");
            result.add(builder.createLineMarkerInfo(element));
        }
    }

}