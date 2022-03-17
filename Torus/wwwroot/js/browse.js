
function populateBrowseCards()
{
    const cardcont = document.getElementById("browse-container");
    var imageURL = "https://thiscatdoesnotexist.com";
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