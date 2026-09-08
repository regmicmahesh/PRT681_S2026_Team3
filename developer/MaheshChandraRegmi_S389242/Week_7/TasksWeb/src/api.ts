import type { CreateTaskPayload, TaskItem } from './types'

const API_BASE = '/api/tasks'

async function readError(response: Response): Promise<string> {
  try {
    const body = await response.json()
    if (body?.title) {
      return body.title
    }
  } catch {
    // The API may return an empty body (for example on 404).
  }

  return `Request failed (${response.status})`
}

export async function getTasks(): Promise<TaskItem[]> {
  const response = await fetch(API_BASE)
  if (!response.ok) {
    throw new Error(await readError(response))
  }
  return response.json()
}

export async function createTask(payload: CreateTaskPayload): Promise<TaskItem> {
  const response = await fetch(API_BASE, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(payload),
  })
  if (!response.ok) {
    throw new Error(await readError(response))
  }
  return response.json()
}

export async function updateTask(task: TaskItem): Promise<TaskItem> {
  const response = await fetch(`${API_BASE}/${task.id}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({
      title: task.title,
      description: task.description,
      isCompleted: task.isCompleted,
    }),
  })
  if (!response.ok) {
    throw new Error(await readError(response))
  }
  return response.json()
}

export async function deleteTask(id: number): Promise<void> {
  const response = await fetch(`${API_BASE}/${id}`, { method: 'DELETE' })
  if (!response.ok) {
    throw new Error(await readError(response))
  }
}
