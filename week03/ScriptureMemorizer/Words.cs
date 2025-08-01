public class Word
{
    public string Text { get; private set; }
    public bool IsHidden { get; private set; }

    public Word(string text)
    {
        Text = text;
        IsHidden = false;
    }

    public void Hide()
    {
        IsHidden = true;
    }

    public void Show()
    {
        IsHidden = false;
    }

    public string GetDisplayText()
    {
        // Retorna underscores com o mesmo número de caracteres da palavra
        return IsHidden ? new string('_', Text.Length) : Text;
    }
}
