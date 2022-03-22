
function populateBrowseCards()
{
    // This function randomly selects placeholder 3D assets from a pre-defined list.
    // As Torus will later feature a backend for useful asset fetching, this function is intended for frontend demonstration purposes.
    var randomPlaceholders = ["donut.png", "liminal_junk.png", "shader_ngloss.png"];
    var assetIndex = Math.floor(Math.random() * randomPlaceholders.length);

    const cardcont = document.getElementById("browse-container");
    var imageURL = "/img/items/" + randomPlaceholders[assetIndex];
    var imageName = "Foobar (3D Model)";
    var newhtml = "";

    newhtml += '<div class="card" style="width:350px">';
    newhtml += `<img class="card-img-top" src="${imageURL}" width="10">`;
    newhtml += `<hr><div class='card-body'>${imageName}</div></div>`;
    cardcont.innerHTML += newhtml;
}


// NOTE TO SELF: cards will be structured like this:

        //<div class="card" style="width:350px">
        //    <img class="card-img-top" src="https://thiscatdoesnotexist.com" width="10">
        //    <div class="card-body">TorusTestThingy</div>
        //</div>