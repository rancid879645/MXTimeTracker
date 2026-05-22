import { get, post, put, del } from './client'

export const getLapTimes = () => get('/laptimes')
export const getLapTime = (id) => get(`/laptimes/${id}`)
export const createLapTime = (data) => post('/laptimes', data)
export const updateLapTime = (id, data) => put(`/laptimes/${id}`, data)
export const deleteLapTime = (id) => del(`/laptimes/${id}`)
