import { RegisterForm } from '@/components/RegisterForm'
import styles from './page.module.css'
import { LoginForm } from '@/components/LoginForm'

export default function Page() {
  return (
    <main className={styles.main}>
      <LoginForm/>
      
    </main>
  )
}
