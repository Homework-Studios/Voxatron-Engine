package homeworkstudios.lang;

import com.intellij.extapi.psi.PsiFileBase;
import com.intellij.openapi.fileTypes.FileType;
import com.intellij.psi.FileViewProvider;
import homeworkstudios.files.VoxaScriptFileType;
import homeworkstudios.files.VoxascriptLanguage;
import org.jetbrains.annotations.NotNull;

public class VoxaScriptFile extends PsiFileBase {

    public VoxaScriptFile(@NotNull FileViewProvider viewProvider) {
        super(viewProvider, VoxascriptLanguage.INSTANCE);
    }

    @NotNull
    @Override
    public FileType getFileType() {
        return VoxaScriptFileType.INSTANCE;
    }

    @Override
    public String toString() {
        return "VoxaScript File";
    }
}
