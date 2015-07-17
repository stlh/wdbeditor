if (typeof x === 'undefined') {
    stlh = {};
}

function getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}

stlh.generatePassword = function (charType, length) {
    var charList = [];
    //Uppercase
    if (charType & 1) {
        for (var i = 65; i < 90; ++i) {
            charList.push(String.fromCharCode(i));
        }
    }
    //Lowercase
    if (charType & 2) {
        for (var i = 97; i < 112; ++i) {
            charList.push(String.fromCharCode(i));
        }
    }
    //Number
    if (charType & 4) {
        for (var j = 0; j < 4; ++j) {
            for (var i = 48; i < 57; ++i) {
                charList.push(String.fromCharCode(i));
            }
        }
    }
    //Special Characters
    if (charType & 8) {
        for (var i = 32; i < 126; ++i) {
            charList.push(String.fromCharCode(i));
        }
    }

    var pw = "";
    for (var i = 0; i < length; ++i) {
        pw += charList[getRandomInt(0, charList.length - 1)];
    }

    return pw;
}