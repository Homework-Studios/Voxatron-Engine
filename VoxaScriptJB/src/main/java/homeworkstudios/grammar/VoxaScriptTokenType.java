package homeworkstudios.grammar;


import com.intellij.psi.tree.IElementType;
import homeworkstudios.files.VoxascriptLanguage;
import org.jetbrains.annotations.NonNls;
import org.jetbrains.annotations.NotNull;

public class VoxaScriptTokenType extends IElementType {
    public VoxaScriptTokenType(@NotNull @NonNls String debugName) {
        super(debugName, VoxascriptLanguage.INSTANCE);
    }

    @Override
    public String toString() {
        return "VoxaScriptTokenType." + super.toString();
    }

}
