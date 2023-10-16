// function extractMenu() {
//     categoryMenu = document.body.querySelector('.category-menu-content-panel__slugs-ede2c')
//     categoryItemBoxes = categoryMenu.querySelectorAll('div.category-menu-group-c2c44')



//     let stringCategories = "";
//     for (let box of categoryItemBoxes) {
//         let tags = box.querySelectorAll('a')

//         let isFirstTagPrinted = false
//         for (tag of tags) {
//             if (!isFirstTagPrinted) {
//                 stringCategories += tag.text + '\n'
//                 isFirstTagPrinted = true
//                 continue
//             }
//             stringCategories += '  ' + tag.text + '\n'
//         }
//         stringCategories += '\n\n\n'
//     }
//     console.log(stringCategories);
// }

function copyStringToClipboard(string) {
    function handler(event) {
        event.clipboardData.setData('text/plain', string);
        event.preventDefault();
        document.removeEventListener('copy', handler, true);
    }

    document.addEventListener('copy', handler, true);
    document.execCommand('copy');
}

function extractMenuToSql(baseParentId) {
    categoryMenu = document.body.querySelector('.category-menu-content-panel__slugs-ede2c')
    categoryItemBoxes = categoryMenu.querySelectorAll('div.category-menu-group-c2c44')

    let queryString = `-- Extracted Category List For ParentId {${baseParentId}}\n`;

    for (let box of categoryItemBoxes) {
        let tags = box.querySelectorAll('a')


        let isFirstTagPrinted = false
        for (tag of tags) {
            if (!isFirstTagPrinted) {

                queryString += `SET @ParentRowId = ${baseParentId};\n`;
                queryString += `INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'${tag.text}', @ParentRowId, '');\n`
                queryString += `SET @ParentRowId = @RowId;\n`;
                queryString += `SET @RowId = @RowId + 1;\n`;

                isFirstTagPrinted = true
                continue
            }

            queryString += `INSERT INTO [dbo].[Categories] (Id, Title, ParentCategoryId, Icon) VALUES (@RowId,N'${tag.text}', @ParentRowId, '');\n`
            queryString += `SET @RowId = @RowId + 1;\n`;

        }
        queryString += '\n\n'
    }
    queryString += "--------------------------------------\n"
    copyStringToClipboard(queryString)
}