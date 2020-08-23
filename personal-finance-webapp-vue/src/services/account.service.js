/* eslint-disable */
import Axios from 'axios';

const RESOURCE_NAME = '/api/Accounts';

export default {
    getAll() {
        return Axios.get(RESOURCE_NAME);
    },

    getAccount(id) {
        return Axios.get(`${RESOURCE_NAME}/${id}`)
    },

    postNewAccount(newAccount) {
        return Axios.post(RESOURCE_NAME, newAccount)
    },

    putUpdate(newData) {
        return Axios.put(RESOURCE_NAME, newData)
    },

    deleteAccount(id) {
        return Axios.delete(`${RESOURCE_NAME}/${id}`)
    }
}