const axios = require('axios');
import { URL_LOGIN, URL_REGISTER } from '@/const/UrlConstants';
import { AxiosError, AxiosResponse } from 'axios';
import { toast } from 'react-toastify';
import Router from 'next/router'


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
    toast.success("Log in succesful!");
    Router.push('/')
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
    Router.push('/')
}

function handleError(error: AxiosError) {
    if (error.response) {
        const errorStatus = error.response.status;
        toast.error(error.response.data as string);
    }

    console.error(error);
}

