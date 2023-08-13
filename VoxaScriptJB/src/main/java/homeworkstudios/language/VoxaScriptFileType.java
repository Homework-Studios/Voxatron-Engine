package homeworkstudios.language;

import com.intellij.openapi.fileTypes.LanguageFileType;
import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;

import javax.swing.*;

public class VoxaScriptFileType extends LanguageFileType {

    public static final VoxaScriptFileType INSTANCE = new VoxaScriptFileType();

    private VoxaScriptFileType() {
        super(VoxaScriptLanguage.INSTANCE);
    }

    @NotNull
    @Override
    public String getName() {
        return "VoxaScript File";
    }

    @NotNull
    @Override
    public String getDescription() {
        return "the scripting language for the VoxaTron engine";
    }

    @NotNull
    @Override
    public String getDefaultExtension() {
        return "vx";
    }

    @Nullable
    @Override
    public Icon getIcon() {
        return VoxaScriptIcons.FILE;
    }

}