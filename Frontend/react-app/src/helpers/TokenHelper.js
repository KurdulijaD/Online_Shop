export const GetToken = () => {
    return sessionStorage.getItem('token') !== null
  }