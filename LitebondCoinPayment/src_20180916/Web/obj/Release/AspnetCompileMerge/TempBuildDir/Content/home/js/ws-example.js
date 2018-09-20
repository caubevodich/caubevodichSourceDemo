var ws = new ReconnectingWebSocket('wss://n.block.io/');
ws.debug = false; // set to true to see messages as they appear
ws.timeout = 5400;

// have we subscribed to the events or not?
// this is just a proof of concept, not a robust implementation
var subscribed = false;

// we'll track transactions per minute aswell
var txpm = {'BTC': [], 'DOGE': [], 'LTC': []}
var coinSymbols = {'BTC': '&#3647;', 'DOGE': '&#208;', 'LTC': '&#321;'}

ws.onmessage = function(msg) {
    // whenever a new message appears, this executes
    msg = JSON.parse(msg.data);
    
    // a new transaction! let's process the data
    if (msg.type == 'new-transactions') { addRowToTable(msg.data); }
    
    if (!subscribed && msg.status == 'success')  {
	// we haven't subscribed yet, and the Notifications server just welcomed us
	subscribed = true;
	subscribeToNewTransactions('BTC'); // bitcoin
	subscribeToNewTransactions('DOGE'); // dogecoin
	subscribeToNewTransactions('LTC'); // litecoin
    }
    
}

function addToTxpm(network) {
    // adds to Transactions per Minute array
    // return Transactions per Minute for given data's network
    
    var curTime = Math.floor(Date.now() / 1000.0);
    var maxTimeOffset = 10*60; // aggregate data over ten minutes, max
    
    txpm[network].push(curTime);
    
    var minTime = curTime - maxTimeOffset;
    
    while (txpm[network][0] < minTime) {
	txpm[network].pop();
    }
    
    var curTxpm = ((txpm[network].length / (curTime - txpm[network][0] + 1)) * 60).toFixed(2);
    
    if (txpm[network].length < 100) { curTxpm = 'gathering data...'; }
    
    $('#txpm-'+network).text(' (Tx/min = ' + curTxpm + ')');
    
    return curTxpm;
}

function subscribeToNewTransactions(network) {
    // send the appropriate JSON string to subscribe to a network's new tx stream
    ws.send(JSON.stringify({'type':'new-transactions','network':network}));
    
}

function addRowToTable(data) {
    // let's add the new transaction to its network's new-transactions table
    var network = data.network;
    
    var sumSent = 0.0; // don't do this, use a BigNumber instead
    
    for (i = 0; i < data.inputs.length; i++) {
	sumSent += parseFloat(data.inputs[i].amount);
    }
    
    
    // add the transaction row to the appropriate table
    $('#bucket-'+network+' tr:first').before('<tr><td><a href="https://chain.so/tx/'+network+'/'+data.txid+'" target=_blank>'+data.txid.substring(0,8)+'...</a></td><td>'+sumSent.toFixed(4)+' '+coinSymbols[network]+'</td></tr>');
    
    $('#bucket-'+network).find('tr:gt(10)').remove(); // keep the first 10 rows, remove the rest
    
    addToTxpm(data.network); // update transactions per minute data
}
