// const createButtons = document.querySelectorAll('.strgamebutton');
// createButtons.forEach((button) => {
//     button.addEventListener('click', () => {
//       window.location.href = 'game1.html';
//     });
//   });


console.log();
const startGameButton = document.getElementById('start-game-button');
startGameButton.addEventListener('click', () => {
    const startGameInput = document.getElementById('start-game');
    const userInput = startGameInput.value;

    if (userInput === 'game1') {
      // 進入遊戲1關卡
      window.location.href = 'game1.html';
    } else if (userInput === 'game2') {
      // 進入遊戲2關卡
      window.location.href = 'game2.html';
    } else if (userInput === 'game3') {
      // 進入遊戲3關卡
      window.location.href = 'game3.html';
    } else {
      // 用戶輸入錯誤，提示錯誤信息
      alert('無效的輸入，請重新輸入！');
    }
});

