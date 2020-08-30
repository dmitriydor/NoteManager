let accessToken = '';

export const setToken = (t: string) => {
  accessToken = t;
};

export const getToken = () => accessToken;
