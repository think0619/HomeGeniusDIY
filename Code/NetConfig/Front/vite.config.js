import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import { resolve } from 'path'

function pathResolve(dir) {
    return resolve(process.cwd(), '.', dir)
}
// https://vitejs.dev/config/
export default defineConfig({
    plugins: [vue()],
    server: { // ← ← ← ← ← ←
        host: '0.0.0.0' // ← 新增内容 ←
    },
    resolve: {
        alias: [{
            find: /@\//,
            replacement: pathResolve('src') + '/'
        }]
    },
    build: {
        chunkSizeWarningLimit: 1500,
    }
})