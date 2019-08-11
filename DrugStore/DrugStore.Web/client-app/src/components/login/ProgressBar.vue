<template>
  <v-progress-circular
      :size="70"
      :width="7"
      color="purple"
      indeterminate
    ></v-progress-circular>
</template>

<script>
  export default {
    name:'progressBar'
  }
</script>

<style>
.v-progress-circular {
  margin: 1rem;
}
</style>

