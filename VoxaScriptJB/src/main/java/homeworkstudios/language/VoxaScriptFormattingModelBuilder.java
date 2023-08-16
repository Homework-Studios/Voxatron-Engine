package homeworkstudios.language;

import com.intellij.formatting.FormattingContext;
import com.intellij.formatting.FormattingModel;
import com.intellij.formatting.FormattingModelBuilder;
import com.intellij.formatting.FormattingModelProvider;
import org.jetbrains.annotations.NotNull;

public class VoxaScriptFormattingModelBuilder implements FormattingModelBuilder {
    @Override
    public @NotNull FormattingModel createModel(FormattingContext formattingContext) {
        return FormattingModelProvider.createFormattingModelForPsiFile(
                formattingContext.getContainingFile(),
                new VoxaScriptBlock(formattingContext.getNode(), null, null),
                formattingContext.getCodeStyleSettings()
        );
    }
}