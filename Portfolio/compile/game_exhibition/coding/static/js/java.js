const form = document.querySelector('form');

// form.addEventListener('submit', (event) => {
//   event.preventDefault();

//   const input = document.querySelector('#StudNumber1');
//   const studNum = input.value;

//   if(studNum.length !== 6){
//     alert('ERROR');
//     return;
//   }

//   window.location.href = 'main.html';

// });

alert('幫助\n stp1:下方 creat 一個玩家帳號\n stp2:登入到達主畫面\n stp3:找到 "#"RGB\n stp4:更多關注解答出密碼')

////跳轉創建
const createButtons = document.querySelectorAll('.create-button');
createButtons.forEach((button) => {
    button.addEventListener('click', () => {
      window.location.href = 'register.html';
    });
  });
