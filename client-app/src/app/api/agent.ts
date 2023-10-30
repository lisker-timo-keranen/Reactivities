import axios, { AxiosResponse } from 'axios';
import { Activity } from '../models/activity'

const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay)
    })
} 

axios.defaults.baseURL = 'http://localhost:5000/api';

axios.interceptors.response.use(async response => {
    try {
        await sleep(1000);
        return response;
    } catch (error) {
        console.log(error);
        return await Promise.reject(error);
    }
})

const responseBody = <T> (response: AxiosResponse<T>) => response.data;

// Määritellään request objekti, joka sisältää kentät (get, post, put, del) jotka ovat metodiviittauksia
// promise palauttaa AxiosResponsen joka annetaan responseBody metodille parametriksi joka lopulta palauttaa response.datan. 
const requests = {
    get: <T> (url: string) => axios.get<T>(url).then(responseBody),
    post: <T> (url: string, body: {}) => axios.post<T>(url, body).then(responseBody),
    put: <T> (url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
    del: <T> (url: string) => axios.delete<T>(url).then(responseBody),
}

// määritellään Activities muuttuja joka sisältää list-kentän, joka on viittaus metodiin
const Activities = {
    list: () => requests.get<Activity[]>('/activities'), // /activities on parametri request.get kentän metodille. get-metodi palauttaa promisen
    details: (id: string) => requests.get<Activity>(`/activities/${id}`),
    create: (activity: Activity) => axios.post<void>('/activities', activity),
    update: (activity: Activity) => axios.put<void>(`activities/${activity.id}`, activity),
    delete: (id: string) => axios.delete<void>(`activities/${id}`)
}

// agent on julkinen jäsen asiakkaille.
const agent = {
    Activities
}

export default agent;