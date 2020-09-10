// для хранения токена в памяти приложения.
// на данный момент не используется

let accessToken = '';

export const setToken = (t: string) => {
  accessToken = t;
};

export const getToken = () => accessToken;
