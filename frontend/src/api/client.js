const BASE_URL = '/api'

async function request(path, options = {}) {
  const token = localStorage.getItem('token')

  const res = await fetch(`${BASE_URL}${path}`, {
    ...options,
    headers: {
      'Content-Type': 'application/json',
      ...(token ? { Authorization: `Bearer ${token}` } : {}),
      ...options.headers,
    },
  })

  if (!res.ok) {
    const body = await res.json().catch(() => ({}))
    throw new Error(body.error || `Request failed: ${res.status}`)
  }

  const text = await res.text()
  return text ? JSON.parse(text) : null
}

export const get = (path) => request(path)
export const post = (path, data) => request(path, { method: 'POST', body: JSON.stringify(data) })
export const put = (path, data) => request(path, { method: 'PUT', body: JSON.stringify(data) })
export const del = (path) => request(path, { method: 'DELETE' })
