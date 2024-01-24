import { RegisterForm } from '@/components/RegisterForm'
import styles from './page.module.css'

export default function Home() {
  return (
    <main className={styles.main}>
      <RegisterForm/>
    </main>
  )
}
