$(document).ready(function() {

    var iterations = 2048;
    
    // generate a random pin, 128 bits long
    pin = CryptoJS.lib.WordArray.random(128/8).toString(CryptoJS.enc.Hex);
    document.getElementById("pin").innerHTML=pin;
    
    // pass the pin through PBKDF2, 128 bit key length, 1,024 iterations of SHA256
    pin_128 = CryptoJS.PBKDF2(pin, "", { keySize: 128/32, iterations: iterations/2, hasher:CryptoJS.algo.SHA256 }).toString(CryptoJS.enc.Hex);
    document.getElementById('pin_hex_128').innerHTML = pin_128;
    
    // pass the result through PBKDF2, 256 bit key length, 1,024 iterations of SHA256
    pin_256 = CryptoJS.PBKDF2(pin_128, "", { keySize: 256/32, iterations: iterations/2, hasher:CryptoJS.algo.SHA256 }).toString(CryptoJS.enc.Hex);
    //      document.getElementById('pin_hex_256').innerHTML = pin_256;
    
    // final encryption key
    document.getElementById('aes_key').innerHTML = String(pin_256);
    
    // example data to encrypt
    data_to_encrypt = 'block.io';
    document.getElementById('data_to_encrypt').innerHTML += data_to_encrypt;
    
    // convert it to its hexadecimal string equivalent, and encrypt it using the pin
    // convert the encrypted data to its base64 string equivalent
    pin_256_hex_obj = CryptoJS.enc.Hex.parse(pin_256);
    enc = CryptoJS.AES.encrypt(data_to_encrypt, pin_256_hex_obj, { mode: CryptoJS.mode.ECB }).ciphertext.toString(CryptoJS.enc.Base64).split(/\s/).join('');
    
    document.getElementById('encrypted_data').innerHTML = enc;
    
    // decrypt the encrypted base64 string
    cipher = CryptoJS.lib.CipherParams.create({ ciphertext: CryptoJS.enc.Base64.parse(enc) });
    dec = CryptoJS.AES.decrypt(cipher, pin_256_hex_obj, {mode: CryptoJS.mode.ECB});
    dec_utf8 = dec.toString(CryptoJS.enc.Utf8);
    
    document.getElementById('decrypted_data').innerHTML = dec_utf8;
    
    // the decrypted data is our private key after a sha256 hash
    passphrase_hex = CryptoJS.enc.Utf8.parse(dec_utf8).toString(CryptoJS.enc.Hex);
    
    document.getElementById('dec_hex_sha256').innerHTML = CryptoJS.SHA256(dec_utf8).toString(CryptoJS.enc.Hex);
    
    // use the passphrase as a private key
    // the blockiojs library use below automatically performs 
    // a sha256 hash of the passphrase_hex and then uses it as a private key
    key = Bitcoin.ECKey.fromPassphrase(passphrase_hex);
    pubkey = key.pub.toHex(); // the resulting public key from the private key
    document.getElementById('pubkey_hex').innerHTML = pubkey;
    
    // let's sign some data, make it 256 bit long
    data_to_sign = 'iSignedThisDataThatIs256BitsLong';
    document.getElementById('data_to_sign').innerHTML = data_to_sign;
    
    // convert the data to hex, this is what we'll sign
    data_to_sign_hex = CryptoJS.enc.Utf8.parse(data_to_sign).toString(CryptoJS.enc.Hex);
    document.getElementById('data_to_sign_hex').innerHTML = data_to_sign_hex;
    
    // sign it using the private key above
    signature = key.sign(data_to_sign_hex);
    document.getElementById('signature').innerHTML = signature;
    
    // the API call to Block.io to verify we recognize the signature as valid
    verify_url = 'https://block.io/api/v2/verify_signature/?signed_data='+data_to_sign_hex+'&signature='+signature+'&public_key='+pubkey;
    
    document.getElementById('verify_link').innerHTML = "<a href='"+verify_url+"' target=_blank rel=noreferrer>Verify Signature</a>";
    document.getElementById('verify_example').innerHTML = verify_url;
});
