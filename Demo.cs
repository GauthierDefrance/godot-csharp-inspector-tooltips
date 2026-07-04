using Godot;

public partial class Demo : Node
{
    /// <summary>
    /// Message displayed by this example project.
    /// Edit this text in the Inspector.
    ///
    /// Leave empty when no message is needed.
    /// </summary>
    [Export]
    public string Message { get; set; } = "Hover over this property name";
}
