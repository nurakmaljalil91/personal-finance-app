<template>
  <div>
    <router-link to="/">Home</router-link>
    <NewAccount />
  </div>
</template>

<script>
import NewAccount from '@/components/NewAccount'
export default {
  name: 'AddAccount',
  components: {
    NewAccount
  }
}
</script>

<style scoped>
</style>
