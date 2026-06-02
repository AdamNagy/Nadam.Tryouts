namespace Rag.Domain;

public interface IRagger
{
    public Task IndexStorage();

    public Task SearchByImage();
}
