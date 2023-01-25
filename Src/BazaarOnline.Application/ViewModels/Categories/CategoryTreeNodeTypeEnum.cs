namespace BazaarOnline.Application.ViewModels.Categories;

public enum CategoryTreeNodeTypeEnum
{
    // hasn't parent but has children
    Root,

    // has parent & children
    Internal,

    // hasn't children
    Leaf,
}