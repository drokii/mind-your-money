const axios = require('axios');
import { URL_LOGIN, URL_REGISTER } from '@/const/UrlConstants';
import { AxiosError, AxiosResponse } from 'axios';
import { useRouter } from 'next/router';
import { toast } from 'react-toastify';
import https from 'https';

export async function logIn(name: string, password: string) {
    axios
        .post(URL_LOGIN, { name, password })
        .then((response: AxiosResponse) => {
            handleLoginSuccess(response);

        })
        .catch((error: AxiosError) => {
            handleError(error);
        });
}

function handleLoginSuccess(response: AxiosResponse<any, any>) {
    localStorage.setItem('jwtToken', response.data);
    useRouter().push('/')
}

export function register(name: string, password: string, email: string) {

    axios
        .post(URL_REGISTER,
            { name, password, email }
        )
        .then(() => {
            handleRegistrationSuccess();
        })
        .catch((error: AxiosError) => {
            handleError(error);
        });
}

function handleRegistrationSuccess() {
    toast.success("Account Created! You can now log in using your credentials.");
    useRouter().push('/login');
}

function handleError(error: AxiosError) {
    if (error.response) {
        const errorStatus = error.response.status;

        const statusMessages: { [key: number]: string } = {
            404: 'The server might not be reachable. Sorry!',
            401: 'Wrong username or password. Try again, bud!',
            400: 'Bad request. You did something that was not meant to be possible. Let the silly developer know he messed up with a screenshot. Thanks!'
        };

        const errorMessage = statusMessages[errorStatus] || 'An unexpected error occurred.';

        toast.error(errorMessage);
    }

    console.error(error);
}

