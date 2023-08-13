package homeworkstudios.language.psi;

import com.intellij.psi.tree.IElementType;
import homeworkstudios.language.VoxaScriptLanguage;
import org.jetbrains.annotations.NonNls;
import org.jetbrains.annotations.NotNull;

public class VoxaScriptTokenType extends IElementType {

    public VoxaScriptTokenType(@NotNull @NonNls String debugName) {
        super(debugName, VoxaScriptLanguage.INSTANCE);
    }

    @Override
    public String toString() {
        return "VoxaScriptTokenType." + super.toString();
    }

}
