import com.intellij.formatting.FormattingContext
import com.intellij.formatting.FormattingModel
import com.intellij.formatting.FormattingModelBuilder
import com.intellij.formatting.FormattingModelProvider
import homeworkstudios.language.VoxaScriptBlock

class VoxaScriptFormattingModelBuilder : FormattingModelBuilder {
    override fun createModel(formattingContext: FormattingContext): FormattingModel {
        return FormattingModelProvider.createFormattingModelForPsiFile(
                formattingContext.containingFile,
                VoxaScriptBlock(formattingContext.node, null, null),
                formattingContext.codeStyleSettings
        )
    }
}