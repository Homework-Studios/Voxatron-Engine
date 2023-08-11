package homeworkstudios.grammar;


import com.intellij.psi.tree.IElementType;
import homeworkstudios.files.VoxascriptLanguage;
import org.jetbrains.annotations.NonNls;
import org.jetbrains.annotations.NotNull;

public class VoxaScriptElementType extends IElementType {

    public VoxaScriptElementType(@NotNull @NonNls String debugName) {
        super(debugName, VoxascriptLanguage.INSTANCE);
    }

}
