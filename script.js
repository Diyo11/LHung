// 獲取所有帶有 data-description 屬性的圖片
const imagesWithDescription = document.querySelectorAll('img[data-description]');

// 對每個圖片添加滑鼠進入和離開事件監聽器
imagesWithDescription.forEach((image) => {
  const description = image.getAttribute('data-description');
  const descriptionDiv = image.nextElementSibling; // 下一個兄弟元素，就是描述的 div

  image.addEventListener('mouseenter', () => {
    descriptionDiv.textContent = description; // 將描述文字添加到 div
    descriptionDiv.classList.remove('d-none'); // 顯示 div
  });

  image.addEventListener('mouseleave', () => {
    descriptionDiv.textContent = ''; // 清空 div 內容
    descriptionDiv.classList.add('d-none'); // 隱藏 div
  });
});
