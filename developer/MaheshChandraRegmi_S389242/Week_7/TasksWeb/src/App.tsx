import { useEffect, useMemo, useState, type FormEvent } from 'react'
import { createTask, deleteTask, getTasks, updateTask } from './api'
import type { TaskItem } from './types'
import './App.css'

function formatWhen(iso: string): string {
  const date = new Date(iso)
  if (Number.isNaN(date.getTime())) {
    return iso
  }

  return new Intl.DateTimeFormat(undefined, {
    month: 'short',
    day: 'numeric',
    hour: '2-digit',
    minute: '2-digit',
  }).format(date)
}

export default function App() {
  const [tasks, setTasks] = useState<TaskItem[]>([])
  const [title, setTitle] = useState('')
  const [description, setDescription] = useState('')
  const [loading, setLoading] = useState(true)
  const [saving, setSaving] = useState(false)
  const [error, setError] = useState<string | null>(null)

  const openCount = useMemo(
    () => tasks.filter((task) => !task.isCompleted).length,
    [tasks],
  )

  async function refresh() {
    const next = await getTasks()
    setTasks(next)
  }

  useEffect(() => {
    let cancelled = false

    getTasks()
      .then((next) => {
        if (!cancelled) {
          setTasks(next)
          setError(null)
        }
      })
      .catch(() => {
        if (!cancelled) {
          setError('Cannot reach the API.')
        }
      })
      .finally(() => {
        if (!cancelled) {
          setLoading(false)
        }
      })

    return () => {
      cancelled = true
    }
  }, [])

  async function handleAdd(event: FormEvent<HTMLFormElement>) {
    event.preventDefault()
    const trimmed = title.trim()
    if (!trimmed) {
      setError('A task needs a title.')
      return
    }

    setSaving(true)
    setError(null)
    try {
      await createTask({
        title: trimmed,
        description: description.trim() || undefined,
      })
      setTitle('')
      setDescription('')
      await refresh()
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Could not add the task.')
    } finally {
      setSaving(false)
    }
  }

  async function handleToggle(task: TaskItem) {
    setError(null)
    try {
      await updateTask({ ...task, isCompleted: !task.isCompleted })
      await refresh()
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Could not update the task.')
    }
  }

  async function handleDelete(id: number) {
    setError(null)
    try {
      await deleteTask(id)
      await refresh()
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Could not delete the task.')
    }
  }

  return (
    <div className="page">
      <header className="mast">
        <div>
          <h1>Desk Queue</h1>
        </div>
        <dl className="count">
          <div>
            <dt>Open</dt>
            <dd>{openCount}</dd>
          </div>
          <div>
            <dt>Total</dt>
            <dd>{tasks.length}</dd>
          </div>
        </dl>
      </header>

      <main className="board">
        <form className="composer" onSubmit={handleAdd}>
          <h2>New ticket</h2>
          <label>
            Title
            <input
              value={title}
              onChange={(event) => setTitle(event.target.value)}
              maxLength={200}
              placeholder="What needs doing?"
              autoComplete="off"
            />
          </label>
          <label>
            Notes
            <textarea
              value={description}
              onChange={(event) => setDescription(event.target.value)}
              maxLength={1000}
              rows={4}
              placeholder="Optional detail"
            />
          </label>
          <button type="submit" disabled={saving}>
            {saving ? 'Filing...' : 'File ticket'}
          </button>
        </form>

        <section className="queue" aria-live="polite">
          <div className="queue-head">
            <h2>Queue</h2>
            <p>Open tickets first. Mark one done when it is finished.</p>
          </div>

          {error && <p className="banner">{error}</p>}

          {loading && (
            <ul className="tickets">
              <li className="ticket skeleton" />
              <li className="ticket skeleton" />
            </ul>
          )}

          {!loading && tasks.length === 0 && !error && (
            <p className="empty">No tickets yet. File one on the left.</p>
          )}

          {!loading && tasks.length > 0 && (
            <ul className="tickets">
              {tasks.map((task) => (
                <li
                  key={task.id}
                  className={task.isCompleted ? 'ticket done' : 'ticket'}
                >
                  <span className="stub">#{String(task.id).padStart(3, '0')}</span>
                  <div className="body">
                    <h3>{task.title}</h3>
                    {task.description && <p>{task.description}</p>}
                    <p className="meta">{formatWhen(task.createdAt)}</p>
                  </div>
                  <div className="actions">
                    <button
                      type="button"
                      className="ghost"
                      onClick={() => handleToggle(task)}
                    >
                      {task.isCompleted ? 'Reopen' : 'Mark done'}
                    </button>
                    <button
                      type="button"
                      className="danger"
                      onClick={() => handleDelete(task.id)}
                    >
                      Delete
                    </button>
                  </div>
                </li>
              ))}
            </ul>
          )}
        </section>
      </main>
    </div>
  )
}
