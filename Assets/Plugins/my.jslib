mergeInto(LibraryManager.library,{





SaveExtern: function(data)
{
  var dataString=UTF8ToString(data);
  var myobj=JSON.parse(dataString);
  player.setData(myobj);
},

LoadExtern: function()
{
if(player){
player.getData().then(_data=>{
if(_data)
{
const myJSON=JSON.stringify(_data);
myGameInstance.SendMessage('YandexDataPlaceLoader','SetPlayerData',myJSON);
}
      });
 }
},

ShowInterstitialExtern: function()
{
	ysdk.adv.showFullscreenAdv({
    callbacks: {
        onClose: function(wasShown) {
          // some action after close
        },
        onError: function(error) {
          // some action on error
        }
    }
})
},

ShowRewardedExtern: function()
{
ysdk.adv.showRewardedVideo({
    callbacks: {
        onOpen: () => {
          console.log('Video ad open.');
        },
        onRewarded: () => {
          console.log('Rewarded!');
          myGameInstance.SendMessage('YandexDataPlaceLoader','GetRewarded');
        },
        onClose: () => {
          console.log('Video ad closed.');
        }, 
        onError: (e) => {
          console.log('Error while open video ad:', e);
        }
    }
})
},

});