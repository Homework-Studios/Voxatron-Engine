package homeworkstudios.language;

import com.intellij.formatting.*;
import com.intellij.psi.codeStyle.CodeStyleSettings;
import homeworkstudios.language.psi.VoxaScriptTypes;
import org.jetbrains.annotations.NotNull;

public class VoxaScriptFormattingModelBuilder implements FormattingModelBuilder {

    private static SpacingBuilder createSpaceBuilder(CodeStyleSettings settings) {
        return new SpacingBuilder(settings, VoxaScriptLanguage.INSTANCE)
                .around(VoxaScriptTypes.SEPARATOR)
                .spaceIf(settings.getCommonSettings(VoxaScriptLanguage.INSTANCE.getID()).SPACE_AROUND_ASSIGNMENT_OPERATORS)
                .before(VoxaScriptTypes.PROPERTY)
                .none();
    }

    @Override
    public @NotNull FormattingModel createModel(@NotNull FormattingContext formattingContext) {
        final CodeStyleSettings codeStyleSettings = formattingContext.getCodeStyleSettings();
        return FormattingModelProvider
                .createFormattingModelForPsiFile(formattingContext.getContainingFile(),
                        new VoxaScriptBlock(formattingContext.getNode(),
                                Wrap.createWrap(WrapType.NONE, false),
                                Alignment.createAlignment(),
                                createSpaceBuilder(codeStyleSettings)),
                        codeStyleSettings);
    }

}
