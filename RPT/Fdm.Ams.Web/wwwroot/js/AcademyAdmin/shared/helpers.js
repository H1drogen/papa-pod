export const objectToForm = (object) => {
  const form = new FormData();
  for (const key in object) form.append(key, object[key]);

  return form;
};

export const toggleSaveResetButtons = (enabled) => {
  const saveButton = document.querySelector(".fc-saveButton-button");
  const resetButton = document.querySelector(".fc-resetButton-button");

  saveButton.disabled = !enabled;
  resetButton.disabled = !enabled;
};
